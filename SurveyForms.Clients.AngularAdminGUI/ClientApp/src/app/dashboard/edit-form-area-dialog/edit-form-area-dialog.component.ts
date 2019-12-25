import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, Validators } from '@angular/forms';
import { IFormArea } from '../../../typings';

@Component({
  selector: 'app-edit-form-area-dialog',
  templateUrl: './edit-form-area-dialog.component.html',
  styleUrls: ['./edit-form-area-dialog.component.css']
})
export class EditFormAreaDialogComponent {
  public form = this.fb.group({
    name: [this.data.name, [Validators.required, Validators.maxLength(50), Validators.minLength(3)]]
  });

  constructor(
    public dialogRef: MatDialogRef<EditFormAreaDialogComponent>,
    @Inject(MAT_DIALOG_DATA)public data: IFormArea,
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
