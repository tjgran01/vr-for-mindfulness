var exports = module.exports = {};

exports.name = "Antis";
exports.mode = "Day";
exports.audio = "track1";
exports.audioVolume = "70";
exports.backgroundVolume = "25";

exports.getData = function() {
  return exports;
};

exports.putData = function(dataModel) {
    exports.name = dataModel.name;
    exports.mode = dataModel.mode;
    exports.audio = dataModel.audio;
    exports.audioVolume = dataModel.audio_Volume;
    exports.backgroundVolume = dataModel.background_Volume;
};