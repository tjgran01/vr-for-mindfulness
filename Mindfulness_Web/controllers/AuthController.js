var mongoose = require("mongoose");
var passport = require("passport");
var User = require("../models/User");
var Authentication = require("../models/AuthenticationID");
var Clinician = require("../models/Clinician");

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

  var type = req.body.user_type;

  if (type == "user") {

    Authentication.find({}, 'code', function (err, objects) {
      if (err) return handleError(err);

      var codes = []
      objects.forEach(function (o) {
        codes.push(o.code);
      });

      var new_code = Math.floor(Math.random() * 900000) + 100000;   //6 digit authentication code

      while (codes.includes(new_code)) {
        new_code = Math.floor(Math.random() * 900000) + 100000;
      }

      var authentication_object = new Authentication({
        _id: new mongoose.Types.ObjectId(),
        code: new_code,
        username: req.body.username
      });

      authentication_object.save(function (err2) {

        if (err2) return handleError(err2);

        User.register(new User({
          username: req.body.username, name: req.body.name,
          location: req.body.location, age: 01, authentication_id: authentication_object._id, isUser: true
        }), req.body.password, function (err, user) {
          if (err) {

            Authentication.remove({ code: new_code }, function (err) { });   //Removing the code object since user not registered
            return res.render('register', {});

          }

          passport.authenticate('userLocal')(req, res, function () {
            res.redirect('/profile');
          });
        });

      });

    });

  }

  else if (type == "clinician") {

    Clinician.register(new Clinician({
      username: req.body.username, name: req.body.name,
      location: req.body.location, age: 01, isClinician: true
    }), req.body.password, function (err, user) {
      if (err) {
        return res.render('register', {});

      }

      passport.authenticate('clinicianLocal')(req, res, function () {
        res.redirect('/dashboard');
      });
    });
  }

};

// Go to login page
userController.login = function (req, res) {
  res.render('login');
};

// Post login

userController.doLogin = function (req, res) {
  var username = req.body.username;
  var type = req.body.user_type;

  if (type == "user") {
    User.findOne({ 'username': username }, 'name', function (err, u) { //query and queried items stored in u
      if (err)
        return handleError(err);

      passport.authenticate('userLocal')(req, res, function () {
        res.redirect('/profile');
      });

    });
  }
  else if (type == "clinician") {

    Clinician.findOne({ 'username': username }, 'name', function (err, u) { //query and queried items stored in u
      if (err)
        return handleError(err);

      passport.authenticate('clinicianLocal')(req, res, function () {
        res.redirect('/dashboard');
      });

    });

  }

  

};

// logout
userController.logout = function (req, res) {
  req.logout();
  //res.redirect('/logout');
  res.render('logout');
};

module.exports = userController;