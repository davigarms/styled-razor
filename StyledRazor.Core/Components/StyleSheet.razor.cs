using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using StyledRazor.Core.Style.StyleSheet;
using System;
using System.Threading.Tasks;

namespace StyledRazor.Core.Components;

public class StyleSheet : ComponentBase, IDisposable
{
  private RenderFragment StyleElement { get; set; }

  protected override void OnInitialized() => StyleSheetService.OnUpdate += UpdateStyleSheet;

  protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, StyleElement);
  
  private async Task UpdateStyleSheet()
  {
    StyleElement = StyleSheetService.CreateStyleSheet(ResetCss);
    await InvokeAsync(StateHasChanged);
  }

  public void Dispose()
  {
    StyleSheetService.Clear();
    StyleSheetService.OnUpdate -= UpdateStyleSheet;
    GC.SuppressFinalize(this);
  }

  private static string ResetCss => @"
    /* Box sizing rules */
    *,
    *::before,
    *::after {
        box-sizing: border-box;
    }

    /* Remove default margin */
    body,
    h1,
    h2,
    h3,
    h4,
    p,
    figure,
    blockquote,
    dl,
    dd {
        margin: 0;
    }

    /* Remove list styles on ul, ol elements with a list role, which suggests default styling will be removed */
    ul[role='list'],
    ol[role='list'] {
        list-style: none;
    }

    /* Set core root defaults */
    html:focus-within {
        scroll-behavior: smooth;
    }

    /* Set core body defaults */
    body {
        min-height: 100vh;
        text-rendering: optimizeSpeed;
        line-height: 1.5;
    }

    /* A elements that don't have a class get default styles */
    a:not([class]) {
        text-decoration-skip-ink: auto;
    }

    /* Make images easier to work with */
    img,
    picture {
        max-width: 100%;
        display: block;
    }

    /* Inherit fonts for inputs and buttons */
    input,
    button,
    textarea,
    select {
        font: inherit;
    }

    /* Remove all animations, transitions and smooth scroll for people that prefer not to see them */
    @media (prefers-reduced-motion: reduce) {
        html:focus-within {
            scroll-behavior: auto;
        }

        *,
        *::before,
        *::after {
            animation-duration: 0.01ms !important;
            animation-iteration-count: 1 !important;
            transition-duration: 0.01ms !important;
            scroll-behavior: auto !important;
        }
    }
  ";
}