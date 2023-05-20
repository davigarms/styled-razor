using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Styling;

public class Background : StyledBase
{
  [Parameter] public string Color { get; set; }
  
  protected override Styled Base => Create.Div(@"
    {
      background: var(--background-color);
      height: inherit; 
    }
  ");

  protected override string Style => $@"
    --background-color: {Color};
  ";
}