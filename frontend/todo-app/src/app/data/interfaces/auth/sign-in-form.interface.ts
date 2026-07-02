import { FormControl } from "@angular/forms";

export interface SignInForm {
    usernameOrEmail: FormControl<string>,
    password: FormControl<string>
}
