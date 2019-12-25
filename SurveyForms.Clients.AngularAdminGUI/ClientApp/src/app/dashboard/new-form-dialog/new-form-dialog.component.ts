import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-new-form-dialog',
  templateUrl: './new-form-dialog.component.html',
  styleUrls: ['./new-form-dialog.component.css']
})
export class NewFormDialogComponent {
  public form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]],
    description: ['', [Validators.maxLength(300)]]
  });

  constructor(public dialogRef: MatDialogRef<NewFormDialogComponent>, @Inject(MAT_DIALOG_DATA)public data: any, private fb: FormBuilder) {
  }

  public closeDialog() {
    this.dialogRef.close();
  }

  public onSubmit() {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
