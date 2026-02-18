import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  template: `
    <input
      type="text"
      placeholder="Search products..."
      (input)="search.emit($any($event.target).value)"
    />
  `,
})
export class SearchBarComponent {
  @Output() search = new EventEmitter<string>();
}
