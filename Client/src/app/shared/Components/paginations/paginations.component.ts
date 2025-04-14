import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-paginations',
  standalone: false,
  templateUrl: './paginations.component.html',
  styleUrl: './paginations.component.scss'
})
export class PaginationsComponent {
@Input() totalCount: number;
@Input() pageSize: number;
@Output() pageChanged = new EventEmitter<number>();

onChangePage(event: any) {
  this.pageChanged.emit(event);
}
}
