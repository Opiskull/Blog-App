import { autoinject, TaskQueue } from "aurelia-framework";
import { IMdcDialogClickEvent, MdcDialog } from "aurelia-mdc-bridge";
import { Post } from "components/blog/post";
import { PostService } from "components/blog/post.service";
import { Router, activationStrategy } from "aurelia-router";

@autoinject()
export class Add {
  private post: Post;
  private dialog: MdcDialog;

  constructor(private taskQueue: TaskQueue, private postService: PostService, private router: Router) {

  }

  private showDialog() {
    this.dialog.foundation.accept = this.onAccept;
    this.dialog.foundation.cancel = this.onCancel;
    this.dialog.show(true);
  }

  private onAccept = (event: Event) => {
    this.postService.create(this.post).then(post => {
      this.post = post;
      this.dialog.show(false);
      this.router.navigate("");
    });
  }

  private onCancel = (event: Event) => {
    this.dialog.show(false);
    this.router.navigate("");
  }

  activate(params: { id: string }) {
    this.post = new Post();
    this.taskQueue.queueTask(() => this.showDialog());
  }

  determineActivationStrategy() {
    return activationStrategy.replace;
  }

  deactivate(){
    document.body.classList.remove("mdc-dialog-scroll-lock");
  }
}
