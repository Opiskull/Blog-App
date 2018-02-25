import { autoinject } from "aurelia-framework";
import { HttpClient, json, RequestInit } from "aurelia-fetch-client";
import { Post } from "./post";

@autoinject()
export class PostService {
  constructor(private http: HttpClient) {

  }

  public get(id: string) {
    return this.http.fetch(`/api/post/${id}`).then(response => response.json());
  }

  public getAll(): Promise<Post[]> {
    return this.http.fetch("/api/post").then(response => response.json());
  }

  public getLatest(): Promise<Post[]> {
    return this.http.fetch("/api/post/latest/3").then(response => response.json());
  }

  public update(post: Post): Promise<Post> {
    return this.http.fetch(`/api/post/${post.id}`, {
      method: "put",
      body: json(post),
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      }
    })
      .then(response => response.json())
  }

  public create(post: Post): Promise<Post> {
    return this.http.fetch('/api/post', {
      method: "post",
      body: json(post),
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      }
    })
      .then(response => response.json())
  }
}
