import { HttpClient } from '@angular/common/http';
import { UserDto } from '../models/';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private usersUrl = 'https://jsonplaceholder.typicode.com/users';

  constructor(private http: HttpClient) { }

  getUsers(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(this.usersUrl);
  }

  getUserById(id: number): Observable<UserDto> {
    const url = `${this.usersUrl}/${id}`;
    return this.http.get<UserDto>(url);
  }
}