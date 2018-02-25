import { bindable, autoinject } from "aurelia-framework";
import { Post } from "./post";
@autoinject()
export class EditPost {
  @bindable()
  private post: Post;

  constructor() {
  }
}
