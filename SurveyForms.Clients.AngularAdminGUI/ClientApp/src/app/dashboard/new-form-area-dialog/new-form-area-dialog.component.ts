import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-new-form-area-dialog',
  templateUrl: './new-form-area-dialog.component.html',
  styleUrls: ['./new-form-area-dialog.component.css']
})
export class NewFormAreaDialogComponent  {
  public form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]]
  });

  constructor(
    public dialogRef: MatDialogRef<NewFormAreaDialogComponent>,
    @Inject(MAT_DIALOG_DATA)public data: any,
    private fb: FormBuilder) {
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
