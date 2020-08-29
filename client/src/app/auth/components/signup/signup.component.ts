import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { SnackbarService } from '../../../shared/services/snackbar.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent implements OnInit {
  form: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private snackbarService: SnackbarService,
    private router: Router
  ) {}

  formInit() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      mail: [
        '',
        [
          Validators.required,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$'),
        ],
      ],
      password: ['', Validators.required],
      retypePassword: ['', Validators.required],
    });
  }

  get mailField() {
    return this.form.get('mail');
  }

  get passwordField() {
    return this.form.get('password');
  }
  get retypePasswordField() {
    return this.form.get('retypePassword');
  }
  get nameField() {
    return this.form.get('name');
  }

  ngOnInit(): void {
    this.formInit();
  }

  goNextStep() {
    this.router.navigate(['dashboard']);
  }

  saveLoggedInUserData(userId) {
    this.authService.getUserInfoById(userId).subscribe((res: any) => {
      if (res.isSuccess) {
        this.authService.loggedInUser.next(res.data);
      }
    });
  }

  submit(data) {

    const userData = {
      Name: data.name,
      Mail: data.mail,
      Password: data.password,
      Role: 'admin'
    };

    this.authService.signUp(userData).subscribe(
      (res: any) => {
        if (res.isSuccess === true) {
          const credential = {
            Mail: data.mail,
            Password: data.password,
          };

          this.authService.getTokenByUserCredential(credential).subscribe(
            (tokenResp: any) => {
              if (tokenResp.isSuccess === true) {
                localStorage.setItem('real_time_chat_token', tokenResp.data.token);
                localStorage.setItem('loggedInUserId', tokenResp.data.userId);
                this.saveLoggedInUserData(tokenResp.data.userId);
                this.goNextStep();
              }
            },
            (err) => {
              this.snackbarService.error();
            }
          );
        }
      },
      (err) => {
        this.snackbarService.error();
      }
    );
  }
}
