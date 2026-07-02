import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../data/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { SignUpForm } from '../../data/interfaces/auth/sign-up-form.interface';
import { FormInputComponent } from "../../common-ui/form-input/form-input.component";

@Component({
    selector: 'app-sign-up-page',
    imports: [ReactiveFormsModule, FormInputComponent, RouterLink],
    templateUrl: './sign-up-page.component.html',
    styleUrl: './sign-up-page.component.css',
})
export class SignUpPageComponent {
    private authService = inject(AuthService);
    private router = inject(Router);

    form = new FormGroup<SignUpForm>({
        username: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        email: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        password: new FormControl('', { nonNullable: true, validators: [Validators.required] })
    });

    onSubmit() {
        if (this.form.valid) {
            const raw = this.form.getRawValue();
            this.authService.signUp(raw).subscribe({
                next: () => {
                    this.router.navigate(['/workspace']);
                },
                error: (err) => {
                    console.error('Sign Up failed: ', err);
                }
            })
        }
    }
}
