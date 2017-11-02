var exports = module.exports = {};

exports.name = "Antis";
exports.mode = "Night";

exports.getData = function() {
  return exports;
};

exports.putData = function(dataModel) {
    exports.name = dataModel.name;
    exports.mode = dataModel.mode;
};