## Facetflow .NET Sample Application ##
This is a sample application written in ASP.NET MVC 4 and using the Elasticsearch .NET client [NEST](https://github.com/Mpdreamz/NEST "NEST") to do basic faceted search and CRUD operations against an index.

### Prerequisites ###
You will need a [Facetflow](https://facetflow.com "Facetflow") (Elasticsearch as a Service) account to get started - which is entirely for free.

Once you sign up you will have to log in to get your connection details which we will need in the next step.

### Configuration ###
The project depends on two application settings stored in Web.config.

**FacetflowUrl**: This is your URL to Elasticsearch. If you look at the C# language guide when you sign in to the Facetflow Control Panel, you can copy the URL directly from there. It will be in the format *https://&lt;apikey&gt;:@&lt;account&gt;.east-us.azr.facetflow.io*.

**FacetflowIndexName**: This is already pre-configured to be *dotnet\_mvc\_sample*, feel free to change it if you'd like.

### Running ###
When the application starts up, it will attempt to create the index and set up any necessary mappings. This is all done from the [Bootstrapper](https://github.com/facetflow/sample-dotnet/blob/master/MvcSample/Bootstrapper.cs) class which also configures dependency injection for MVC using Unity.

**Indexing**:
Start off by indexing a new [Post](https://github.com/facetflow/sample-dotnet/blob/master/MvcSample/Models/Post.cs) - this is the document type that we are using for the purpose of this example. You can index a new post by clicking **Create Post** in the top navigation. We will use whatever you put in the *Category* field to create a simple [Terms Facet](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-facets-terms-facet.html) on the search page for you to use to navigate your posts.

**Searching**: You can now search for your posts either by keyword (search box at the top right hand corner), or by faceted navigation from the Category facet (or both).

## Deploying ###
The simplest way is to sign up for a [Windows Azure](http://www.windowsazure.com/ "Windows Azure") account and deploy the project to a free Windows Azure Website. 

Put the essential configuration in Web.config and push the project to a private Git repository. Then configure your Windows Azure website to deploy directly from your Git repository.
