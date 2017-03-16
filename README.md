# ThenInclude
ThenInclude like in .Net core for EntityFramework 6

```csharp
var thenInclude = context.One.Include(x => x.Twoes)
    .ThenInclude(x=> x.Threes)
    .ThenInclude(x=> x.Fours)
    .ThenInclude(x=> x.Fives)
    .ThenInclude(x => x.Sixes)
    .Include(x=> x.Other)
    .ToList();
```