mergeInto(LibraryManager.library, {
  FaceAPI_SetReceiver: function (ptr) {
    var name = UTF8ToString(ptr);
    if (typeof window !== 'undefined' && window.FaceAPI && window.FaceAPI.setReceiver) {
      window.FaceAPI.setReceiver(name);
    }
  },

  FaceAPI_Start: function () {
    if (typeof window !== 'undefined' && window.FaceAPI && window.FaceAPI.start) {
      window.FaceAPI.start();
    }
  },

  FaceAPI_Stop: function () {
    if (typeof window !== 'undefined' && window.FaceAPI && window.FaceAPI.stop) {
      window.FaceAPI.stop();
    }
  }
});
