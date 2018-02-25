import { Store } from "aurelia-store";
import { State } from "./../../src/state/app-state";
import { App } from '../../src/app';
import { Container } from "aurelia-framework";
import { PostService } from "components/blog/post.service";

describe('the app', () => {
  it('says hello', () => {
    var container = new Container();

    expect(new App(new Store<State>({ posts: [] }), container.get(PostService)).state.posts.length === 0);
  });
});
