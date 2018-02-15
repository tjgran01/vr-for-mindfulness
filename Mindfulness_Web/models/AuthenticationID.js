var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var AuthenticationSchema = new Schema({
    _id: mongoose.Schema.Types.ObjectId,
    code: Number,
    username: String
},
{
    collection: 'codes'
  });


module.exports = mongoose.model('AuthenticationID', AuthenticationSchema);