import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../data/services/auth.service';
import { inject } from '@angular/core';
import { catchError, map, of } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    if (authService.isAuthenticated()) {
        return true;
    }

    if (authService.refreshToken()) {
        return authService.refresh().pipe(
            map(() => true),
            catchError(() => {
                authService.clearToken();
                return of(router.createUrlTree(['/auth/sign-in']));
            })
        );
    }

    return router.createUrlTree(['/auth/sign-in']);
};
