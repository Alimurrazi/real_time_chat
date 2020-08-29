import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private _snackBar: MatSnackBar) { }

  error(msg?) {
    if (!msg) {
      msg = 'Something went wrong';
    }
    this._snackBar.open(msg, null, {
      duration: 2000,
    });
  }
}
