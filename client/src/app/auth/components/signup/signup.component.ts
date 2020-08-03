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
      mail: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    this.formInit();
  }

  getNextStep() {
    this.router.navigate(['dashboard']);
  }

  submit(data) {
    this.authService.logIn(data).subscribe((res:any) => {
      if(res.isSuccess === true){
        debugger;
        localStorage.setItem('loggedInUserId', res.data.userId);
      }
      console.log(res);
      debugger;
    });
  }

}
