# WikiWalker
> Web crawler that finds the shortest path between two Wikipedia articles.
## Features
- Uses breadth-first search (BFS) approach (explores paths by going down one level at a time: it first looks at all links on the starting page, then looks at each link of each child, etc)
- You can either input link, or just a name of the article
- Async approach, makes you not deadlock, while WikiWalker finds a path
## Dependencies
 - HtmlAgilityPack
 
## Screenshots 
Example of input 
```cs
var path = await Walker.GetPathAsync(@"Minecraft", @"C_Sharp_(programming_language)");
```
![alt text](https://github.com/D3AD-E/WikiWalker/blob/master/Pic1.png?raw=true)
