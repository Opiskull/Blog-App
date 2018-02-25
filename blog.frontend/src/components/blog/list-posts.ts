import { State } from "./../../state/app-state";
import { Post } from "./post";
import { bindable, autoinject } from "aurelia-framework";
import { PostService } from "./post.service";
import { Store } from "aurelia-store";
import { Router } from "aurelia-router";


const addAction = (state: State, newPost: Post) => {
  const newState = Object.assign({}, state);
  newState.posts.push(newPost);
  return newState;
}

@autoinject()
export class ListPosts {
  @bindable() posts: Post[];

  constructor(private postService: PostService, private store: Store<State>, private router: Router) {
    store.registerAction("AddAction", addAction);
  }

  public newPost() {
    this.router.navigateToRoute("new-post");
  }

  public editPost(id: string) {
    this.router.navigateToRoute("edit-post", { id: id })
  }
}
