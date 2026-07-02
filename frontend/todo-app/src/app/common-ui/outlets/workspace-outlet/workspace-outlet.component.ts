import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { SvgIconComponent } from "../../svg-icon/svg-icon.component";
import { AuthService } from '../../../data/services/auth.service';

@Component({
    selector: 'app-workspace-outlet',
    imports: [RouterOutlet, SvgIconComponent],
    templateUrl: './workspace-outlet.component.html',
    styleUrl: './workspace-outlet.component.css',
})
export class WorkspaceOutletComponent {
    private authService = inject(AuthService);
    private router = inject(Router);

    signOut() {
        this.authService.signOut().subscribe({
            next: () => {
                this.router.navigate(['/auth/sign-in']);
            },
            error: (err) => {
                console.error("Error while signing out:", err);
                this.authService.clearToken();
                this.router.navigate(['/auth/sign-in']);
            }
        })
    }
}
