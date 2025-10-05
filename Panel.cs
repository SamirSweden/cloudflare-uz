// Pages/Panel.cs


@page "/panel"
@using System.Net.Http.Json
@inject HttpClient Http
@inject NavigationManager Nav

<PageTitle>🚀 Cloudflare | The best hosting</PageTitle>

<section class="banner bg-black mb-[50px] overflow-x-hidden overflow-y-hidden" >
    <div class="container">
      
        <div class="flex  justify-center ml-[100px]">

            <div class="card__content grid gap-3 grid-cols-3 lg:grid-cols-3 md:grid-cols-3 sm:grid-cols-2">

                @foreach (var product in products)
                {
                    <div class="card px-[25px] bg-[#171717] rounded-[20px]">
                        <div class="bg-[#171717] flex flex-col gap-8 p-4 md:p-8 rounded-[15px]">
                            <h3 class="text-white font-medium text-[32px]">&mdash;@product.Name</h3>
                            <div>
                                <ul class="flex flex-col gap-6 mt-8 text-white">
                                    <li>@product.Api</li>
                                    <li>Net speed @product.InetSpeed</li>
                                    <li>@product.L7</li>
                                </ul>
                                <div class="mt-6 border-t py-6 text-[#787878]">
                                    <span class="text-white font-black text-[40px]">@product.Price</span>
                                </div>
                                <div class="flex items-center w-full">
                                    <button class="text-white w-full bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700
                                               font-medium rounded-lg text-sm px-5 py-2.5"
                                            @onclick="() => Purchase(product)">
                                        Purchase
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                }
            </div>
        </div>
    </div>
</section>

@code {
    public class Hosting
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string InetSpeed { get; set; }
        public string L7 { get; set; }
        public string Api { get; set; }
    }

    List<Hosting> products = new()
    {
        new Hosting { Name = "LITE", Price = "25$", InetSpeed = "🚀 320 mbit/s" , L7 = "L7" , Api = "❌ Api Excluded"},
        new Hosting { Name = "VIP", Price = "600$", InetSpeed = "🚀 500 mbit/s", L7 = "L7 & L4" , Api = "✅ Api Included"},
        new Hosting { Name = "BUSINESS", Price = "700$", InetSpeed = "🚀 500 mbit/s", L7 = "L7 & L4" , Api = "✅ Api Included"},
        new Hosting { Name = "PREMIER", Price = "4500$", InetSpeed = "🚀 500 mbit/s", L7 = "L7 & L4" , Api = "✅ Api Included"},
        new Hosting { Name = "PREMIER+", Price = "4700$", InetSpeed = " 🚀500 mbit/s", L7 = "L7 & L4" , Api = "✅ Api Included"},
        new Hosting { Name = "LUXURY", Price = "5000$", InetSpeed = "🚀 500 mbit/s" ,L7 = "L7 & L4", Api = "✅ Api Included"}
    };

    async Task Purchase(Hosting product)
    {
        await Http.PostAsJsonAsync("api/cart", product);
        Nav.NavigateTo("/dashboard");
    }
}
