//Layout/MainLayout.cs 



@inherits LayoutComponentBase
@inject  NavigationManager Nav


<div class="page">
    <!--
        <div class="sidebar">
            <NavMenu/>
        </div>
    -->
    
    
    <aside class="header bg-black">
        <NavMenu />
    </aside>
    <main class="bg-[#000]">
        <div class="top-row px-4 bg-black">
            <div class="flex items-center gap-4">
            </div>
        </div>

        <article class="content px-4">
            @Body
        </article>
    </main>
</div>

<div id="blazor-error-ui">
    An unhandled error has occurred.
    <a href="" class="reload">Reload</a>
    <a class="dismiss">🗙</a>
</div>


