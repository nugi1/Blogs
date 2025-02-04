export class Blog{
  id: string = '';
  title: string = '';
  content: string = '';
  publishedAt: Date = new Date();
  username: string = '';
  comments: { text: string; author: string }[] = [];

  constructor(title: string, content: string, publishedAt: Date, username: string) {
    this.title = title;
    this.content = content;
    this.publishedAt = publishedAt;
    this.username = username;
    this.comments = [];
  }
}
