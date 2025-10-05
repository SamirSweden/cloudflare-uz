// Pages/Home.cs 


@page "/"
@inject NavigationManager Nav


<PageTitle>Home</PageTitle>

<section class="main py-[20px] px-[0px] h-[90vh]">
    <div class="container">
        <div class="main__inner flex items-center h-[50%] justify-center">
            <div class="main__content w-[620px] shadow-[0_0_20px_rgba(255,255,255,0.2)] rounded-medium p-[20px] ">
                <h1 class="font-black text-blue-300 
                        outline-none border-none capitalize
                        user-select-none text-7xl mb-4
                ">Соединяйте</h1>
                <h1 class="font-black text-blue-300  ml-[100px]
                        outline-none border-none lowercase
                        user-select-none text-5xl mb-2
                ">защищайте</h1>
                <h1 class="font-black text-yellow-300 
                        outline-none border-none lowercase
                        user-select-none text-4xl
                ">Соединяйте</h1>
                <div class="flex items-center gap-3 w-full mt-4">
                    <button @onclick="() => WhiteBtn()"
                            class="text-black bg-white capitalize text-center  
                            rounded-md py-[16px] px-[100px] font-semibold 
                            outline-none border-none cursor-pointer w-full">
                        Pricing
                    </button>
                    <button @onclick="() => ConnectBtn()"
                            class="text-black bg-orange-600 capitalize text-center  
                            rounded-md py-[16px] px-[100px] font-semibold 
                            outline-none border-none cursor-pointer w-full hover:shadow-xl ">
                        Connect
                    </button>
                </div>
            </div>
        </div>
    </div>
</section>



@code{

    public void WhiteBtn()
    {
        Nav.NavigateTo("/panel");
    }
    
    public void ConnectBtn()
    {
        Nav.NavigateTo("/connect");
    }

}
