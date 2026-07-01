import { Component, inject } from '@angular/core';
import { AuthService } from '../../data/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { SignInForm } from '../../data/interfaces/sign-in-form.interface';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormInputComponent } from "../../common-ui/form-input/form-input.component";

@Component({
    selector: 'app-sign-in-page',
    imports: [FormInputComponent, ReactiveFormsModule, RouterLink],
    templateUrl: './sign-in-page.component.html',
    styleUrl: './sign-in-page.component.css',
})
export class SignInPageComponent {
    private authService = inject(AuthService);
    private router = inject(Router);

    form = new FormGroup<SignInForm>({
        usernameOrEmail: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        password: new FormControl('', { nonNullable: true, validators: [Validators.required] })
    });

    onSubmit() {
        if (this.form.valid) {
            const raw = this.form.getRawValue();
            this.authService.signIn(raw).subscribe({
                next: () => {
                    this.router.navigate(['/workspace']);
                },
                error: (err) => {
                    console.error('Sign In failed: ', err);
                }
            })
        }
    }
}
