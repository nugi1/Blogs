export class Blog{
  id: string = '';
  title: string = '';
  content: string = '';
  publishedAt: Date = new Date();
  author: string = '';

  constructor(title: string, content: string, publishedAt: Date, author: string) {
    this.title = title;
    this.content = content;
    this.publishedAt = publishedAt;
    this.author = author;
  }
}
