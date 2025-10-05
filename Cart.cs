// Pages/Cart.cs


@page "/dashboard"
@using System.Net.Http.Json
@inject HttpClient Http

<PageTitle>Dashboard</PageTitle>

<div class="product ">
    <div class="container">
        <div class="product__wrap flex justify-center items-center">
            
            <div class="product__width w-[500px]">
                <h1 class="text-white text-2xl mb-6">&mdash; Your Purchases</h1>

                @if (products == null)
                {
                    <p class="text-gray-300">Loading...</p>
                }
                else if (products.Count == 0)
                {
                    <p class="text-gray-300">No purchases yet.</p>
                }
                else
                {
                    <ul class="space-y-4">
                        @foreach (var item in products)
                        {
                            <li class="bg-[#171717] p-4 rounded-[24px] shadow text-white flex items-center justify-between">
                                <strong>@item.Name</strong> - @item.Price <br />
                                @item.InetSpeed - @item.L7 - @item.Api
                                <button @onclick="() => RemoveItem(item)"
                                        class="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-[12px]"
                                ><span class="remove__btn w-[20px]"><i class="fas fa-trash-alt"></i></span></button>
                            </li>
                            
                           
                        }
                    </ul>
                  
                }
            </div>
           
        </div>
    </div>
</div>

@code {
    public class Hosting
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string InetSpeed { get; set; }
        public string L7 { get; set; }
        public string Api { get; set; }
    }

    private List<Hosting>? products;

    protected override async Task OnInitializedAsync()
    {
        products = await Http.GetFromJsonAsync<List<Hosting>>("api/cart");
    }


    private void RemoveItem(Hosting item)
    {
        products?.Remove(item);
    }    
}
