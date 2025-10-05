// Pages/Connect.cs



@page "/connect"
@inject HttpClient Http
@inject IJSRuntime JS 
@inject NavigationManager Navigation

<PageTitle>🚀 Connect website</PageTitle>

<div class="banner">
    <div class="container">
        <div class="banner__inner">
            <div class="content">
                <EditForm Model="model" OnValidSubmit="AddSite">
                    <InputText
                        @bind-Value="model.Url"
                        placeholder="https://example.com"
                        class="banner__input"/>
                    <div class="flex items-center w-full">
                        <button type="submit" class="banner__btn">
                            Добавить
                        </button>
                    </div>
                </EditForm>
            </div>
        </div>
    </div>
</div>

<style>
    .content {
        padding : 35px;
        background: #171717;
        border-radius: 25px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.6);
    }
    .banner__btn {
        background: #FF7B01 !important;
        padding: 16px 32px;
        outline: none;
        border: none;
        cursor: pointer;
        border-radius: 17px;
        width: 100%;
        font-size: 17px;
        font-family:  "Arial",sans-serif;
    }
    .banner__input {
        padding: 13px 18px !important;
        border-radius: 14px;
        width: 400px;
        border: none;
        color: white;
        margin-bottom: 20px;
        outline:none;
        background: #111;
    }
    .banner{
        height: 90vh;
        background: #000;
    }
    .banner__inner{
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100%;
    }
</style>

<script>
    window.showToast = (message, isError = false) => {
        const toast = document.createElement('div');
        toast.innerText = message;
        toast.style.position = 'fixed';
        toast.style.bottom = '20px';
        toast.style.right = '20px';
        toast.style.background = isError ? 'red' : 'green';
        toast.style.color = 'white';
        toast.style.padding = '15px 20px';
        toast.style.borderRadius = '8px';
        toast.style.zIndex = 9999;
        toast.style.boxShadow = '0 4px 12px rgba(0,0,0,0.3)';
        document.body.appendChild(toast);
        setTimeout(() => { document.body.removeChild(toast); }, 3000);
    };
</script>

@code {
    private SiteModel model = new();

    private async Task AddSite()
    {
        if (!string.IsNullOrWhiteSpace(model.Url))
        {
            try
            {
                var urlToSend = model.Url;
                if (!urlToSend.StartsWith("http://") && !urlToSend.StartsWith("https://"))
                {
                    urlToSend = "https://" + urlToSend;
                }

                var response = await Http.PostAsJsonAsync("api/Sites/add", urlToSend);
                
                if (response.IsSuccessStatusCode)
                {
                    await JS.InvokeVoidAsync("showToast", $"{model.Url} успешно зарегистрирован!");
                    model.Url = "";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await JS.InvokeVoidAsync("showToast", $"Ошибка: {error}", true);
                }
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("showToast", $"Ошибка подключения: {ex.Message}", true);
            }
        }
        else
        {
            await JS.InvokeVoidAsync("showToast", "Введите URL сайта", true);
        }
    }

    class SiteModel
    {
        public string Url { get; set; } = "";
    }
}
