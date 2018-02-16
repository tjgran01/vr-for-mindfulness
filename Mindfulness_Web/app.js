var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');
var mongoose = require('mongoose');
var passport = require('passport');
var LocalStrategy = require('passport-local').Strategy;

  mongoose.Promise = global.Promise;
  //  mongoose.connect('mongodb://abhijit93:abhijit93@ds111568.mlab.com:11568/mindfulness_testing')
  //    .then(() =>  console.log('connection to remote database succesful'))
  //    .catch((err) => console.error(err));
  mongoose.connect('mongodb://localhost/localPempDB')
     .then(() =>  console.log('connection to local database succesful'))
     .catch((err) => console.error(err));

var index = require('./routes/index');
var profile = require('./routes/profile');
var dashboard = require('./routes/dashboard');

var app = express();

// view engine setup
app.set('views', path.join(__dirname, 'views'));
//app.use(express.static(__dirname + '/public'));
app.set('view engine', 'ejs');

//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(require('express-session')({
    secret: 'secretSessionKey',
    resave: false,
    saveUninitialized: false
}));
app.use(passport.initialize());
app.use(passport.session());

app.use(express.static(path.join(__dirname, 'public')));

app.use('/', index);
app.use('/profile', profile);
app.use('/dashboard', dashboard);

// passport configuration
// var User = require('./models/User');
// passport.use(new LocalStrategy(User.authenticate()));
// passport.serializeUser(User.serializeUser());
// passport.deserializeUser(User.deserializeUser());

var User = require('./models/User');
var Clinician = require('./models/Clinician');
passport.use('userLocal', new LocalStrategy(User.authenticate()));
passport.use('clinicianLocal', new LocalStrategy(Clinician.authenticate()));
//passport.use(new LocalStrategy(User.authenticate(), Clinician.authenticate()));
// passport.serializeUser(User.serializeUser(), Clinician.serializeUser());
// passport.deserializeUser(User.deserializeUser(), Clinician.deserializeUser());

passport.serializeUser(function(user, done) { 
  done(null, user);
});

passport.deserializeUser(function(user, done) {
  if(user!=null)
    done(null,user);
});

// // passport configuration
// var Clinician = require('./models/Clinician');
// passport.use(new LocalStrategy(Clinician.authenticate()));
// passport.serializeUser(Clinician.serializeUser());
// passport.deserializeUser(Clinician.deserializeUser());

// catch 404 and forward to error handler
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

module.exports = app;
