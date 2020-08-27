import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  form: FormGroup;
  constructor(private formBuilder: FormBuilder,
              private authService: AuthService,
              private router: Router) { }

  formInit() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      mail: ['', [Validators.required,
        Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$')
      ]],
      password: ['', Validators.required],
      retypePassword: ['', Validators.required]
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
    this.authService.logIn(data).subscribe((res: any) => {
      if (res.isSuccess === true){
        localStorage.setItem('real_time_chat_token', res.data.token);
        localStorage.setItem('loggedInUserId', res.data.userId);
        this.saveLoggedInUserData(res.data.userId);
        this.goNextStep();
      }
    });
  }

}
