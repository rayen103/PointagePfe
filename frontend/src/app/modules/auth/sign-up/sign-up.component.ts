import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import {
    AbstractControl,
    FormsModule,
    NgForm,
    ReactiveFormsModule,
    UntypedFormBuilder,
    UntypedFormGroup,
    ValidationErrors,
    Validators,
} from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Router, RouterLink } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

/** Les deux mots de passe doivent correspondre. */
function passwordsMatch(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirm = control.get('passwordConfirm')?.value;
    return password && confirm && password !== confirm ? { passwordsMismatch: true } : null;
}

@Component({
    selector: 'auth-sign-up',
    templateUrl: './sign-up.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
    standalone: true,
    imports: [
        RouterLink,
        FormsModule,
        ReactiveFormsModule,
        MatProgressSpinnerModule,
    ],
})
export class AuthSignUpComponent implements OnInit {
    @ViewChild('signUpNgForm') signUpNgForm: NgForm;

    alert: { type: FuseAlertType; message: string } = {
        type: 'success',
        message: '',
    };
    signUpForm: UntypedFormGroup;
    showAlert: boolean = false;
    isLoading: boolean = false;

    constructor(
        private _authService: AuthService,
        private _formBuilder: UntypedFormBuilder,
        private _router: Router
    ) {}

    ngOnInit(): void {
        this.signUpForm = this._formBuilder.group(
            {
                nomSociete: ['', [Validators.required, Validators.maxLength(50)]],
                nom: ['', [Validators.required, Validators.maxLength(50)]],
                prenom: ['', [Validators.required, Validators.maxLength(50)]],
                email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
                password: ['', [Validators.required, Validators.minLength(8)]],
                passwordConfirm: ['', Validators.required],
                agreements: [false, Validators.requiredTrue],
            },
            { validators: passwordsMatch }
        );
    }

    signUp(): void {
        if (this.signUpForm.invalid || this.isLoading) {
            return;
        }

        this.isLoading = true;
        this.showAlert = false;

        const { nomSociete, nom, prenom, email, password } = this.signUpForm.value;

        this._authService.registerCompany({ nomSociete, nom, prenom, email, password }).subscribe({
            next: (response) => {
                this.isLoading = false;
                if (response?.success) {
                    // Le code de vérification a été envoyé : place à la saisie du code.
                    this._router.navigate(['/verify-code'], { queryParams: { email: email.trim().toLowerCase() } });
                } else {
                    this.showErrorAlert(response?.message || "L'inscription a échoué. Veuillez réessayer.");
                }
            },
            error: (error: HttpErrorResponse) => {
                this.isLoading = false;
                const message =
                    error?.error?.message ||
                    (error.status === 0
                        ? 'Impossible de se connecter au serveur. Vérifiez votre connexion internet.'
                        : "L'inscription a échoué. Veuillez réessayer plus tard.");
                this.showErrorAlert(message);
            },
        });
    }

    private showErrorAlert(message: string): void {
        this.alert = { type: 'error', message };
        this.showAlert = true;
    }
}
