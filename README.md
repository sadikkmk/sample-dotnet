## Facetflow .NET Sample Application ##
This is a sample application written in ASP.NET MVC 4 and using the Elasticsearch .NET client [NEST](https://github.com/Mpdreamz/NEST "NEST") to do basic faceted search and CRUD operations against an index.

### Prerequisites ###
You will need a [Facetflow](https://facetflow.com "Facetflow") (Elasticsearch as a Service) account to get started - which is entirely for free.

Once you sign up you will have to log in to get your connection details which we will need in the next step.

### Configuration ###
The project depends on two application settings. If you're running the site locally you can set these up in Web.config. Otherwise we recommend that you use the **Configuration feature** of Windows Azure Websites described in the [Deployment](#deploying) section of this document.

**FacetflowUrl**: This is your URL to Elasticsearch. If you look at the C# language guide when you sign in to the Facetflow Control Panel, you can copy the URL directly from there. It will be in the format *https://&lt;apikey&gt;:@&lt;account&gt;.east-us.azr.facetflow.io*.

**FacetflowIndexName**: This is already pre-configured to be *dotnet\_mvc\_sample*, feel free to change it if you'd like.

### Running ###
When the application starts up, it will attempt to create the index and set up any necessary mappings. This is all done from the [Bootstrapper](https://github.com/facetflow/sample-dotnet/blob/master/MvcSample/Bootstrapper.cs) class which also configures dependency injection for MVC using Unity.

**Indexing**:
Start off by indexing a new [Post](https://github.com/facetflow/sample-dotnet/blob/master/MvcSample/Models/Post.cs) - this is the document type that we are using for the purpose of this example. You can index a new post by clicking **Create Post** in the top navigation. We will use whatever you put in the *Category* field to create a simple [Terms Facet](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-terms-facet.html) on the search page for you to use to navigate your posts.

**Searching**: You can now search for your posts either by keyword (search box at the top right hand corner), or by faceted navigation from the Category facet (or both).

## Deploying ###
Sign up for a [Windows Azure](http://www.windowsazure.com/ "Windows Azure") account and create a free Windows Azure Website from the Azure Management Portal. 

When you've created your site, make sure you have forked this repository on GitHub and then:

1. Click **Set up deployment from source control** from the site's getting started page. 
2. Pick **GitHub** in the list when asked where your source code is.
3. Choose the **sample-dotnet** repository from the repository name list.

The sample site is now being deployed to your Azure Website, however you need to configure your Facetflow connection details before it will work.

1. Go into the **Configuration** tab in the Azure top navigation
2. Scroll all the way down to **app settings**.
3. Add a new key called **FacetflowUrl**, set the value to your cluster URL in the *https://&lt;apikey&gt;:@&lt;account&gt;.east-us.azr.facetflow.io* format.
4. Click **Save** in the bottom tool bar.

Your site is now being refreshed and will soon be up and running with your configuration.