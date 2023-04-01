import { Component } from '@angular/core';
import { UserDto } from './models';
import { UserService } from './service/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  users: UserDto[] = [];
  constructor(
    private userService: UserService
  ){}

  ngOnInit() {
    this.userService.getUsers().subscribe((usersResult: UserDto[])=>{
      this.users = usersResult;
    });
  }
  counter(i: number) {
    return new Array(i);
  }
}
