mergeInto(LibraryManager.library, {
    FBInteropInit: function(unityInterop, leaderboardName) {
        this.unityInterop = Pointer_stringify(unityInterop);
        this.leaderboardName = Pointer_stringify(leaderboardName);
        this.preloadedRewardedVideo = null;
        console.log("Init interop: " + this.unityInterop + " : " + this.leaderboardName);
    },
    FBInteropLeaderboardRetrieveScore: function() {
        console.log("Retrieving score");
        var playerID = FBInstant.player.getID();
        FBInstant.getLeaderboardAsync(this.leaderboardName).then(function(leaderboard) {
            console.log("Retrieved leaderboard: " + leaderboard);
            leaderboard.getEntriesAsync(10, 0).then(function(entries) {
                console.log("Retrieved entries: " + entries);
                if (entries.length > 0) {
                    SendMessage(this.unityInterop, "UnityInteropLeaderboardScoreRetrieved", entries[0].getScore());
                    console.log(entries[0].getScore());
                }
            });
        }).catch(function(error) {
            console.error("ERROR: " + error);
        });
    },
    FBInteropLeaderboardAddNewScore: function(score) {
        FBInstant.getLeaderboardAsync(this.leaderboardName).then(function(leaderboard) {
            console.log('Saving score');
            return leaderboard.setScoreAsync(score, FBInstant.player.getName());
        }).then(function() {
            console.log('Score saved')
            SendMessage(this.unityInterop, "UnityInteropLeaderboardScoreAdded");
        }).catch(function(error) {
            console.error(error);
        });
    },
    FBInteropRequestReward: function(id) {
        var idd = Pointer_stringify(id);
        this.preloadedRewardedVideo = null;
        console.log("Getting reward video: " + idd);
        FBInstant.getRewardedVideoAsync(idd).then(function(rewarded) {
            console.log("Retrieved reward video: " + rewarded);
            // Load the Ad asynchronously
            this.preloadedRewardedVideo = rewarded;
            return this.preloadedRewardedVideo.loadAsync();
        }).then(function() {
            console.log("Loaded reward video: " + idd);
            SendMessage(this.unityInterop, "UnityInteropRewardLoaded");
        }).catch(function(err) {
            console.error('Rewarded video failed to preload: ' + err.message);
        });
    },
    FBInteropShowReward: function(id) {
        var idd = Pointer_stringify(id);
        console.log("Show reward video: " + idd);
        this.preloadedRewardedVideo.showAsync().then(function() {
            // Perform post-ad success operation
            console.log("Showed reward video: " + idd);
            SendMessage(this.unityInterop, "UnityInteropRewardShown");
        }).catch(function(e) {
            console.error(e.message);
        });
    },
});