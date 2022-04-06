# Myflix - Your personal database for movies and TV shows
Myflix is a web application which offers users the possiblity to keep track of their watched movies from all platforms, review, rate and recommend them. The platform is also meant to be a social space, where the user can communicate with their friends and get new movies ideas from his friends' watched movies and reviews.

# Technology
  
  The web application API is built with ASP.NET Core and uses Microsoft SQL database.
  
  The clien side of the web app is based on VueJs framework together with Bootstrap 5. Check out the repository for frontend -> https://github.com/TudorGabriel28/myflix-Vue-Client

# Interface and functionalities

There are two types of accounts: user and admin. The user account contains all the customer functionalities of the web application, while the admin has additional capabilities like:
* Get, Create, Update and Delete any user
* Delete movies

More details will follow in [Admin Pages](#admin-pages)

![First page](https://user-images.githubusercontent.com/61846679/148544865-3732e07b-1974-484c-b808-6ba1e6641ab7.png)

## Login

After you registered and confirmed your email address, you can login into your account.

![Login Page](https://user-images.githubusercontent.com/61846679/148546055-1562595c-f101-45e3-aeff-ef9f9f818c8a.png)

## Main page
 
 Here you can see all the movies added by you in your lists. You can search for any movie in the database, read about it and if you want to see it in the future add it in your wish list or watched list if you have already seen the movie.
 If the movie doesn't exist in the myflix database you can add it through "Add Movie" page.
 
 ![Home page](https://user-images.githubusercontent.com/61846679/148546694-61e2b537-85e9-4e42-857f-6dcc4e94ea8b.png)
 
## Add a movie
 The method in which you can add a new movie is the following: 
  The user needs to go to the Imdb Webpage of the desired movie and copy the part of the URL that contains the movie Id. This id always starts with "tt" letters followed by 7 digits. After this, he will paste the copied id in the secific field from the ![Add movie] page and the movie is added to the database. This feature is possible by making a request to the Data-Imdb Api from RapidAPI.
  This is a free API service that provides a JSON object with all the details of the movie. You can read more about this API here: https://rapidapi.com/SAdrian/api/data-imdb1/
  
![Add movie page](https://user-images.githubusercontent.com/61846679/158264438-8d3e59c0-c7b4-4405-9e6d-da04e053d48b.png)

## Friends

The secondary purpose of this web app, besides keeping an archive with all of your seen or wish to see movies, is to also be a community. In this sense, you can be friends with other users and see their favourites movies. (if the user has a public account)

![Friends page](https://user-images.githubusercontent.com/61846679/148549658-ca91f256-0ca0-4854-ace9-a45c2bcb654c.png)

## Admin pages

### Users

On Users page an admin can view, search or delete any user in the database.

![Users page](https://user-images.githubusercontent.com/61846679/148549810-92750a86-4db4-46bb-abf8-2c41e8dd67c9.png)

### Movie Requests

On this page the admin will be able to see the movies added to the database and delete them.

![Requests page](https://user-images.githubusercontent.com/61846679/158267882-98971d49-e073-43bf-a148-e56e462f16b9.png)

# What are the next steps

On the backend, all the basic functionality was implemented and the following tasks will be completed in the near future:
- [ ] Friends functionality
- [ ] Chats between users
- [ ] Section for user comments at each movie





