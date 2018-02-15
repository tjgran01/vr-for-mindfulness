var mongoose = require("mongoose");
var passport = require("passport");
var User = require("../models/User");
var Authentication = require("../models/AuthenticationID");

var userController = {};

// Restrict access to root page
userController.home = function (req, res) {
  res.render('index', { user: req.user });
};

// Go to registration page
userController.register = function (req, res) {
  res.render('register');
};

// Post registration
userController.doRegister = function (req, res) {

  Authentication.find({}, 'code', function (err, objects) {

    if (err) return handleError(err);

    var codes = []
    objects.forEach(function(o){
      codes.push(o.code);
    });

    var new_code = Math.floor(Math.random()*900000) + 100000;   //6 digit authentication code

    while(codes.includes(new_code)){
      new_code = Math.floor(Math.random()*900000) + 100000;
    }

    var authentication_object = new Authentication({
      _id: new mongoose.Types.ObjectId(),
      code: new_code,  
      username: req.body.username
    });
  
    authentication_object.save(function(err2){
  
      if (err2) return handleError(err2);
  
      User.register(new User({ username: req.body.username, name: req.body.name, 
        location: req.body.location, age: req.body.age, authentication_id : authentication_object._id }), req.body.password, function (err, user) {
        if (err) {

          Authentication.remove({ code: new_code }, function (err) {});   //Removing the code object since user not registered
          return res.render('register', { user: user });
          
        }
    
        passport.authenticate('local')(req, res, function () {
          res.redirect('/profile');
        });
      });
  
    });

  });

  
  
  
};

// Go to login page
userController.login = function (req, res) {
  res.render('login');
};

// Post login

userController.doLogin = function (req, res) {
  var username = req.body.username;
  User.findOne({ 'username': username }, 'name time', function (err, u) { //query and queried items stored in u
    if (err)
      return handleError(err);
    try {
      var utime = u.time;
      var currentDateTime = req.body.date;
      utime = utime + "$$" + currentDateTime;
      u.time = utime;
      u.save(function (err) {
        if (err)
          return handleError(err); // saved!
      });
    }
    catch (Exception) {
      res.render('login');
    }

  })

  
  passport.authenticate('local')(req, res, function () {
    res.redirect('/profile');
  });
  //write function to again db and add timestamp
};

userController.logEvent = function (req, res) {
  //console.log(req);
  if (req.user != null) {
    //console.log(req.body);
    var actionString = req.body.eventType;
    var timeString = req.body.time;
    var tags = req.body.tags;

    User.findOne({ 'username': req.user.username }, 'name actions', function (err, u) { //query and queried items stored in u
      if (err)
        return handleError(err);
      else {
        var temp = null;
        if (actionString == "PageLoaded" && tags != "") {
          temp = u.actions;
          var x = actionString + "#" + timeString;
          x = x + "**" + tags;
          temp = temp + "$$" + x;
        }
        else {
          if (u.actions == "") {
            temp = actionString + "#" + timeString;
          } else {
            temp = u.actions;
            var x = actionString + "#" + timeString;
            temp = temp + "$$" + x;
          }
        }
        u.actions = temp;
        u.save(function (err) {
          if (err)
            return handleError(err); // saved!
        });

      }

    });
  }
}

// logout
userController.logout = function (req, res) {
  req.logout();
  //res.redirect('/logout');
  res.render('logout');
};

module.exports = userController;