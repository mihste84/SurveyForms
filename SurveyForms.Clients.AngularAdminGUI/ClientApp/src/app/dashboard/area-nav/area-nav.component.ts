import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IFormArea } from '../../../typings';

@Component({
  selector: 'app-area-nav',
  templateUrl: './area-nav.component.html',
  styleUrls: ['./area-nav.component.css']
})
export class AreaNavComponent {
  @Input() public areas: IFormArea[];
  @Input() public selectedAreaId: number;

  @Output() public selectFormAreaCallback = new EventEmitter<IFormArea>();

  public selectFormArea(area: IFormArea) {
    if (this.selectedAreaId !== area.formAreaId) {
      this.selectFormAreaCallback.emit(area);
    }
  }
}
