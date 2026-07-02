import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { BehaviorSubject, catchError, filter, switchMap, take, throwError } from 'rxjs';
import { AuthService } from './services/auth.service';

let refreshTokenSubject$ = new BehaviorSubject<string | null>(null);
let isRefreshing = false;

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    if (req.url.includes('/auth/') && !req.url.includes('/auth/sign-out')) {
        return next(req);
    }

    const token = authService.accessToken();
    let authReq = req;

    if (token) {
        authReq = addToken(req, token);
    }

    return next(authReq).pipe(
        catchError((error) => {
            if (error instanceof HttpErrorResponse && error.status === 401) {
                return handle401Error(authService, req, next);
            }
            return throwError(() => error);
        })
    );
};

const handle401Error = (
    authService: AuthService,
    req: HttpRequest<any>,
    next: HttpHandlerFn
) => {
    if (!isRefreshing) {
        isRefreshing = true;
        refreshTokenSubject$.next(null);

        return authService.refresh().pipe(
            switchMap((res) => {
                isRefreshing = false;
                refreshTokenSubject$.next(res.accessToken); 
                
                return next(addToken(req, res.accessToken));
            }),
            catchError((refreshError) => {
                isRefreshing = false;
                authService.signOut();
                return throwError(() => refreshError);
            })
        );
    }

    return refreshTokenSubject$.pipe(
        filter(token => token !== null),
        take(1),
        switchMap((newToken) => {
            return next(addToken(req, newToken!));
        })
    );
};

const addToken = (req: HttpRequest<any>, token: string) => {
    return req.clone({
        setHeaders: {
            Authorization: `Bearer ${token}`
        }
    });
};
