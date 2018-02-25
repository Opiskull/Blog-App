import { autoinject } from "aurelia-framework";
import { RoutableComponentActivate, activationStrategy, Router } from "aurelia-router";
import { PostService } from "../components/blog/post.service";
import { Post } from "../components/blog/post";
import { MdcDialog, IMdcDialogClickEvent } from 'aurelia-mdc-bridge';
@autoinject()
export class Edit implements RoutableComponentActivate {
  private post: Post;
  private dialog: MdcDialog;

  constructor(private postService: PostService, private router: Router) {

  }

  private showDialog() {
    this.dialog.foundation.accept = this.onAccept;
    this.dialog.foundation.cancel = this.onCancel;
    this.dialog.show(true);
  }

  private onAccept = (event: Event) => {
    this.postService.update(this.post).then(post => {
      this.post = post;
      this.dialog.show(false);
      this.router.navigate("");
    })
  }

  private onCancel = (event: Event) => {
    this.dialog.show(false);
    this.router.navigate("");
  }

  activate(params: { id: string }) {
    this.postService.get(params.id)
      .then(post => this.post = post)
      .then(post => this.showDialog());
  }

  determineActivationStrategy() {
    return activationStrategy.replace;
  }

  deactivate(){
    document.body.classList.remove("mdc-dialog-scroll-lock");
  }
}
