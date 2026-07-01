import { HttpClient } from '@angular/common/http';
import { inject, Service, signal } from '@angular/core';
import { TokenResponse } from '../interfaces/token-response.interface';
import { SignUpRequest } from '../interfaces/sign-up-request.interface';
import { tap } from 'rxjs';
import { SignInRequest } from '../interfaces/sign-in-request.interface';
import { CookieService } from 'ngx-cookie-service';

@Service()
export class AuthService {
    private baseUrl = 'http://localhost:10000/auth'
    private http = inject(HttpClient);
    private cookies = inject(CookieService)

    private readonly ACCESS_TOKEN_KEY = 'access_token';
    private readonly REFRESH_TOKEN_KEY = 'refresh_token';

    accessToken = signal<string | null>(
        this.cookies.get(this.ACCESS_TOKEN_KEY) || null
    );
    refreshToken = signal<string | null>(
        this.cookies.get(this.REFRESH_TOKEN_KEY) || null
    );

    signUp(request: SignUpRequest) {
        return this.http.post<TokenResponse>(`${this.baseUrl}/sign-up`, request)
        .pipe(tap(response => this.setToken(response)));
    }

    signIn(request: SignInRequest) {
        return this.http.post<TokenResponse>(`${this.baseUrl}/sign-in`, request)
        .pipe(tap(response => this.setToken(response)));
    }

    signOut() {
        return this.http.post<void>(
            `${this.baseUrl}/sign-out`,
            JSON.stringify(this.refreshToken),
            { headers: { 'Content-Type': 'application/json' }
        })
        .pipe(tap(() => this.clearToken()));
    }

    refresh() {
        return this.http.post<TokenResponse>(
            `${this.baseUrl}/refresh`,
            JSON.stringify(this.refreshToken),
            { headers: { 'Content-Type': 'application/json' }
        })
        .pipe(tap(response => this.setToken(response)));
    }

    isAuthenticated() {
        return !!this.accessToken();
    }

    private setToken(response: TokenResponse) {
        const cookieOptions = {
            path: '/',
            sameSite: 'Strict' as const,
            expires: 30
        };

        this.cookies.set(this.ACCESS_TOKEN_KEY, response.accessToken, cookieOptions);
        this.cookies.set(this.REFRESH_TOKEN_KEY, response.refreshToken, cookieOptions);

        this.accessToken.set(response.accessToken);
        this.refreshToken.set(response.refreshToken);
    }

    private clearToken() {
        this.cookies.delete(this.ACCESS_TOKEN_KEY, '/');
        this.cookies.delete(this.REFRESH_TOKEN_KEY, '/');

        this.accessToken.set(null);
        this.refreshToken.set(null);
    }
}
