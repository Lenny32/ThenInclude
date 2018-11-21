# ThenInclude
ThenInclude like in .Net core for EntityFramework 6

## Download
To install ThenInclude.EF6, run the following command in the Package Manager Console:
    
	PM> Install-Package ThenInclude.EF6


Example

```csharp
using System.Data.Entity.Include;

var thenInclude = context.One.Including(x => x.Twoes)
    .ThenInclude(x=> x.Threes)
    .ThenInclude(x=> x.Fours)
    .ThenInclude(x=> x.Fives)
    .ThenInclude(x => x.Sixes)
    .Including(x=> x.Other)
    .ToList();
```
