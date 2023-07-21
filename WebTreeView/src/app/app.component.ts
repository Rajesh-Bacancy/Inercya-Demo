import { Component } from '@angular/core';
import * as items from '../inputJson/Items.json';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'web-tree-view';
  list: any = (items as any).default;
  
  constructor()
  {

  }
}
