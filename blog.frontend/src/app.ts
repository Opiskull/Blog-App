import { autoinject, ComponentAttached, ComponentDetached, PLATFORM, } from "aurelia-framework";
import { PostService } from "./components/blog/post.service";
import { Post } from "./components/blog/post";
import { State } from "./state/app-state";
import { Subscription } from "rxjs/Rx";
import { Store } from "aurelia-store";
import { RouteConfig, Router } from "aurelia-router";


const loadAction = (state: State, posts: Post[]) => {
  const newState = Object.assign({}, state);
  newState.posts = posts;
  return newState;
}

@autoinject()
export class App implements ComponentAttached, ComponentDetached {
  public state: State;
  private subscription: Subscription;

  constructor(private store: Store<State>, private postService: PostService) {
    this.store.registerAction("LoadAction", loadAction);
  }

  attached() {
    this.subscription = this.store.state.subscribe(
      (state: State) => this.state = state
    );
    this.postService.getAll().then(posts => this.store.dispatch(loadAction, posts));
  }

  detached() {
    this.subscription.unsubscribe();
  }

  configureRouter(config: RouteConfig, router: Router) {
    config.map([
      {
        route: ['', 'latest'], name: "latest", viewPorts:
          { primary: { moduleId: PLATFORM.moduleName("./routes/list") }, secondary: { moduleId: null } }
      },
      {
        route: 'new-post', name: "new-post", viewPorts:
          { primary: { moduleId: PLATFORM.moduleName("./routes/list") }, secondary: { moduleId: PLATFORM.moduleName("./routes/new") } }
      },
      {
        route: 'edit-post/:id', name: "edit-post", viewPorts:
          { primary: { moduleId: PLATFORM.moduleName("./routes/list") }, secondary: { moduleId: PLATFORM.moduleName("./routes/edit") } }
      }
    ])

  }
}
