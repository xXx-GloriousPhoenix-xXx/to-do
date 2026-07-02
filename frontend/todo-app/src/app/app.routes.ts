import { Routes } from '@angular/router';
import { AuthOutletComponent } from './common-ui/outlets/auth-outlet/auth-outlet.component';
import { SignUpPageComponent } from './pages/sign-up-page/sign-up-page.component';
import { SignInPageComponent } from './pages/sign-in-page/sign-in-page.component';
import { WorkspacePageComponent } from './pages/workspace-page/workspace-page.component';
import { WorkspaceOutletComponent } from './common-ui/outlets/workspace-outlet/workspace-outlet.component';
import { TodoPageComponent } from './pages/todo-page/todo-page.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { authGuard } from './common-ui/auth.guard';
import { guestGuard } from './common-ui/guest.guard';

export const routes: Routes = [
    { path: '', redirectTo: 'auth/sign-up', pathMatch: 'full' },
    {
        path: 'auth',
        component: AuthOutletComponent,
        children: [
            { path: 'sign-up', component: SignUpPageComponent },
            { path: 'sign-in', component: SignInPageComponent }
        ],
        canActivate: [guestGuard]
    },
    {
        path: 'workspace',
        component: WorkspaceOutletComponent,
        children: [
            { path: '', component: WorkspacePageComponent },

            { 
                path: 'todo',
                children: [
                    { path: 'create', component: TodoPageComponent },
                    { path: ':id', component: TodoPageComponent },
                ]
            },

        ],
        canActivate: [authGuard]
    },
    {
        path: 'profile',
        component: ProfilePageComponent,
        canActivate: [authGuard]
    }
];
