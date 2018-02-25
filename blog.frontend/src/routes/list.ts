import { Post } from "components/blog/post";
import { autoinject } from "aurelia-dependency-injection";
import { RoutableComponentActivate } from "aurelia-router";
import { PostService } from "components/blog/post.service";
@autoinject()
export class List implements RoutableComponentActivate {
  public posts: Post[];

  constructor(private postService: PostService) {

  }

  public activate() {
    this.postService.getAll().then(posts => this.posts = posts);
  }
}
