import { Component, OnInit } from '@angular/core';
import { IFormArea, IForm } from '../../../typings';
import { MatDialog } from '@angular/material/dialog';
import { NewFormAreaDialogComponent } from '../new-form-area-dialog/new-form-area-dialog.component';
import { AuthHttpService } from '../../security/auth-http.service';
import { ApiUrls } from '../../constants/api-urls';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NewFormDialogComponent } from '../new-form-dialog/new-form-dialog.component';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.css']
})
export class DashboardPageComponent implements OnInit {
  public areas: IFormArea[];
  public selectedArea: IFormArea;
  public selectedForms: IForm[];

  constructor(private dialog: MatDialog, private http: AuthHttpService, private snackBar: MatSnackBar) { }

  public selectFormArea(area: IFormArea, refresh: boolean = false) {
    if (!refresh && this.selectedArea && area.formAreaId === this.selectedArea.formAreaId) { return; }

    this.http.get<IForm[]>(ApiUrls.Form + `/${area.formAreaId}`).subscribe(_ => {
      this.selectedForms = _;
      this.selectedArea = area;
    });
  }

  public editFormArea() {
  }

  public addNewForm() {
    const dialogRef = this.dialog.open(NewFormDialogComponent, {
      width: '550px', disableClose: true
    });

    dialogRef.afterClosed().subscribe((model: { name: string, description: string}) => {
      if (model) {
        this.http.put(ApiUrls.Form, { areaId: this.selectedArea.formAreaId, ...model }).subscribe(() => {
          this.refreshForms();
          this.getAllFormAreas();
          this.openSnackbar(`Successfully created form '${model.name}'`, 'Dismiss', 5000);
        });
      }
    });
  }

  public addNewFormArea() {
    const dialogRef = this.dialog.open(NewFormAreaDialogComponent, {
      width: '350px'
    });

    dialogRef.afterClosed().subscribe((model: { name: string }) => {
      if (model) {
        this.http.put<number>(ApiUrls.FormArea, model).subscribe(_ => {
          this.getAllFormAreas();
          this.openSnackbar(`Successfully created form area '${model.name}'`, 'Dismiss', 5000);
        });
      }
    });
  }

  public getAllFormAreas() {
    this.http.get<IFormArea[]>(ApiUrls.FormArea).subscribe(_ => {
      this.areas = _;
    });
  }

  public openSnackbar(message: string, action: string, duration: number) {
    this.snackBar.open(message, action, { duration });
  }

  public deleteFormClick(form: IForm) {
    if (!confirm(`Are you sure you want to delete form '${form.name}'?`)) { return; }

    this.http.delete(ApiUrls.Form + '/' + form.formId).subscribe(res => {
      this.openSnackbar(`Deleted form '${form.name}'.`, 'Dismiss', 5000);
      const areaIndex = this.areas.findIndex(_ => _.formAreaId === this.selectedArea.formAreaId);
      this.areas[areaIndex].formCount--;

      this.refreshForms();
    });
  }

  public refreshForms() {
    this.http.get<IForm[]>(ApiUrls.Form + `/${this.selectedArea.formAreaId}`).subscribe(_ => this.selectedForms = _);
  }

  ngOnInit() {
    this.areas = [];
    this.getAllFormAreas();
  }
}
