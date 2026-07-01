import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../data/services/auth.service';

export const guestGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    if (authService.isAuthenticated() || authService.refreshToken()) {
        return router.createUrlTree(['/workspace']);
    }

    return true;
};
