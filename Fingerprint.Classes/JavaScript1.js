(function () {
    var refs = {};;
    var aliases = {};
    aliases["window.navigator.userAgent"] = eval("window.navigator.userAgent");
    aliases["window.navigator"] = eval("window.navigator");
    aliases["window.navigator.language"] = eval("window.navigator.language");
    aliases["window.navigator.geolocation"] = eval("window.navigator.geolocation");
    aliases["window.navigator.webkitTemporaryStorage"] = eval("window.navigator.webkitTemporaryStorage");
    aliases["window.navigator.webkitPersistentStorage"] = eval("window.navigator.webkitPersistentStorage");
    aliases["window.navigator.getBattery"] = eval("window.navigator.getBattery");
    aliases["window.navigator.sendBeacon"] = eval("window.navigator.sendBeacon");
    aliases["window.navigator.getGamepads"] = eval("window.navigator.getGamepads");
    aliases["window.navigator.javaEnabled"] = eval("window.navigator.javaEnabled");
    aliases["window.navigator.vibrate"] = eval("window.navigator.vibrate");
    aliases["window.navigator.permissions"] = eval("window.navigator.permissions");
    aliases["window.navigator.credentials"] = eval("window.navigator.credentials");
    aliases["window.navigator.mediaDevices"] = eval("window.navigator.mediaDevices");
    aliases["window.navigator.serviceWorker"] = eval("window.navigator.serviceWorker");
    aliases["window.navigator.storage"] = eval("window.navigator.storage");
    aliases["window.navigator.presentation"] = eval("window.navigator.presentation");
    aliases["window.navigator.requestMediaKeySystemAccess"] = eval("window.navigator.requestMediaKeySystemAccess");
    aliases["window.navigator.getUserMedia"] = eval("window.navigator.getUserMedia");
    aliases["window.navigator.webkitGetUserMedia"] = eval("window.navigator.webkitGetUserMedia");
    aliases["window.navigator.requestMIDIAccess"] = eval("window.navigator.requestMIDIAccess");;;
    (function () {
        var resp = null;
        delete window.screen;
        Object.defineProperty(window, "screen", {
            configurable: true,
            enumerable: false,
            get: function () {
                if (resp) return resp;
                resp = (function () {
                    var res = {};
                    var prot = {};
                    if (res) Object.defineProperty(res, 'toString', {
                        configurable: true,
                        enumerable: false,
                        get: function () {
                            return function () {
                                return "[object Screen]"
                            }
                        }
                    });
                    if (res) Object.defineProperty(res, 'valueOf', {
                        configurable: true,
                        enumerable: false,
                        get: function () {
                            return function () {
                                return "[object Screen]"
                            }
                        }
                    });
                    Object.defineProperty(res, "orientation", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return (function () {
                                var res = {};
                                var prot = {};
                                if (res) Object.defineProperty(res, 'toString', {
                                    configurable: true,
                                    enumerable: false,
                                    get: function () {
                                        return function () {
                                            return "[object ScreenOrientation]"
                                        }
                                    }
                                });
                                if (res) Object.defineProperty(res, 'valueOf', {
                                    configurable: true,
                                    enumerable: false,
                                    get: function () {
                                        return function () {
                                            return "[object ScreenOrientation]"
                                        }
                                    }
                                });
                                prot["angle"] = 0;
                                prot["type"] = "portrait-primary";
                                prot["onchange"] = null;
                                prot["lock"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function lock() { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function lock() { [native code] }"
                                    };
                                    return res;
                                })();
                                prot["unlock"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function unlock() { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function unlock() { [native code] }"
                                    };
                                    return res;
                                })();
                                prot["addEventListener"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function addEventListener() { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function addEventListener() { [native code] }"
                                    };
                                    return res;
                                })();
                                prot["removeEventListener"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function removeEventListener() { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function removeEventListener() { [native code] }"
                                    };
                                    return res;
                                })();
                                prot["dispatchEvent"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function dispatchEvent() { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function dispatchEvent() { [native code] }"
                                    };
                                    return res;
                                })();
                                Object.setPrototypeOf(res, prot);
                                return res;
                            })();
                        }
                    });
                    prot["availWidth"] = 393;
                    prot["availHeight"] = 851;
                    prot["width"] = 393;
                    prot["height"] = 851;
                    prot["colorDepth"] = 24;
                    prot["pixelDepth"] = 24;
                    prot["availLeft"] = 0;
                    prot["availTop"] = 0;
                    Object.setPrototypeOf(res, prot);
                    return res;
                })();;
                return resp;
            }
        })
    })();;
    (function () {
        var resp = null;
        delete window.navigator;
        Object.defineProperty(window, "navigator", {
            configurable: true,
            enumerable: false,
            get: function () {
                if (resp) return resp;
                resp = (function () {
                    var res = {};
                    var prot = {};
                    if (res) Object.defineProperty(res, 'toString', {
                        configurable: true,
                        enumerable: false,
                        get: function () {
                            return function () {
                                return "[object Navigator]"
                            }
                        }
                    });
                    if (res) Object.defineProperty(res, 'valueOf', {
                        configurable: true,
                        enumerable: false,
                        get: function () {
                            return function () {
                                return "[object Navigator]"
                            }
                        }
                    });
                    Object.defineProperty(res, "maxTouchPoints", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return 5
                        }
                    });
                    Object.defineProperty(res, "doNotTrack", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return "1"
                        }
                    });
                    Object.defineProperty(res, "connection", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return (function () {
                                var res = {};
                                var prot = {};
                                if (res) Object.defineProperty(res, 'toString', {
                                    configurable: true,
                                    enumerable: false,
                                    get: function () {
                                        return function () {
                                            return "[object NetworkInformation]"
                                        }
                                    }
                                });
                                if (res) Object.defineProperty(res, 'valueOf', {
                                    configurable: true,
                                    enumerable: false,
                                    get: function () {
                                        return function () {
                                            return "[object NetworkInformation]"
                                        }
                                    }
                                });
                                prot["onchange"] = null;
                                prot["effectiveType"] = "4g";
                                prot["rtt"] = 50;
                                prot["downlink"] = 10;
                                prot["saveData"] = true;
                                prot["downlinkMax"] = null;
                                prot["type"] = "wifi";
                                prot["ontypechange"] = null;
                                prot["addEventListener"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function addEventListener() { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function addEventListener() { [native code] }"
                                    };
                                    return res;
                                })();
                                prot["removeEventListener"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function removeEventListener() { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function removeEventListener() { [native code] }"
                                    };
                                    return res;
                                })();
                                prot["dispatchEvent"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function dispatchEvent() { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function dispatchEvent() { [native code] }"
                                    };
                                    return res;
                                })();
                                Object.setPrototypeOf(res, prot);
                                return res;
                            })();
                        }
                    });
                    Object.defineProperty(res, "deviceMemory", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return 4
                        }
                    });
                    Object.defineProperty(res, "contacts", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return (function () {
                                var res = {};
                                var prot = {};
                                if (res) Object.defineProperty(res, 'toString', {
                                    configurable: true,
                                    enumerable: false,
                                    get: function () {
                                        return function () {
                                            return "[object ContactsManager]"
                                        }
                                    }
                                });
                                if (res) Object.defineProperty(res, 'valueOf', {
                                    configurable: true,
                                    enumerable: false,
                                    get: function () {
                                        return function () {
                                            return "[object ContactsManager]"
                                        }
                                    }
                                });
                                prot["getProperties"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function () { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function () { [native code] }"
                                    };
                                    return res;
                                })();
                                prot["select"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function () { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function () { [native code] }"
                                    };
                                    return res;
                                })();
                                Object.setPrototypeOf(res, prot);
                                return res;
                            })();
                        }
                    });
                    Object.defineProperty(res, "canShare", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return (function () {
                                var res = function () { };
                                res.toString = function () {
                                    return "function () { [native code] }"
                                };
                                res.valueOf = function () {
                                    return "function () { [native code] }"
                                };
                                return res;
                            })()
                        }
                    });
                    Object.defineProperty(res, "share", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return (function () {
                                var res = function () { };
                                res.toString = function () {
                                    return "function () { [native code] }"
                                };
                                res.valueOf = function () {
                                    return "function () { [native code] }"
                                };
                                return res;
                            })()
                        }
                    });
                    Object.defineProperty(res, "wakeLock", {
                        configurable: true,
                        enumerable: true,
                        get: function () {
                            return (function () {
                                var res = {};
                                var prot = {};
                                if (res) Object.defineProperty(res, 'toString', {
                                    configurable: true,
                                    enumerable: false,
                                    get: function () {
                                        return function () {
                                            return "[object WakeLock]"
                                        }
                                    }
                                });
                                if (res) Object.defineProperty(res, 'valueOf', {
                                    configurable: true,
                                    enumerable: false,
                                    get: function () {
                                        return function () {
                                            return "[object WakeLock]"
                                        }
                                    }
                                });
                                prot["request"] = (function () {
                                    var res = function () { };
                                    res.toString = function () {
                                        return "function () { [native code] }"
                                    };
                                    res.valueOf = function () {
                                        return "function () { [native code] }"
                                    };
                                    return res;
                                })();
                                Object.setPrototypeOf(res, prot);
                                return res;
                            })();
                        }
                    });
                    prot["vendorSub"] = "";
                    prot["productSub"] = "20030107";
                    prot["vendor"] = "Google Inc.";
                    prot["cookieEnabled"] = true;
                    prot["appCodeName"] = "Mozilla";
                    prot["appName"] = "Netscape";
                    prot["appVersion"] = "5.0 (Linux; Android 10; MI 9) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Mobile Safari/537.36";
                    prot["platform"] = "Linux armv8l";
                    prot["product"] = "Gecko";
                    prot["userAgent"] = aliases["window.navigator.userAgent"];
                    prot["language"] = aliases["window.navigator.language"];
                    prot["languages"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "ru-RU,ru,en-US,en"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "ru-RU,ru,en-US,en"
                                }
                            }
                        });
                        Object.defineProperty(res, "0", {
                            configurable: true,
                            enumerable: true,
                            get: function () {
                                return aliases["window.navigator.language"]
                            }
                        });
                        Object.defineProperty(res, "1", {
                            configurable: true,
                            enumerable: true,
                            get: function () {
                                return "ru"
                            }
                        });
                        Object.defineProperty(res, "2", {
                            configurable: true,
                            enumerable: true,
                            get: function () {
                                return "en-US"
                            }
                        });
                        Object.defineProperty(res, "3", {
                            configurable: true,
                            enumerable: true,
                            get: function () {
                                return "en"
                            }
                        });
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["onLine"] = true;
                    prot["geolocation"] = (function () {
                        var res = aliases["window.navigator.geolocation"];
                        return res;
                    })();;
                    prot["mediaCapabilities"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object MediaCapabilities]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object MediaCapabilities]"
                                }
                            }
                        });
                        prot["decodingInfo"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function decodingInfo() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function decodingInfo() { [native code] }"
                            };
                            return res;
                        })();
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["webkitTemporaryStorage"] = (function () {
                        var res = aliases["window.navigator.webkitTemporaryStorage"];
                        return res;
                    })();;
                    prot["webkitPersistentStorage"] = (function () {
                        var res = aliases["window.navigator.webkitPersistentStorage"];
                        return res;
                    })();;
                    prot["getBattery"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.getBattery"]) {
                                return aliases["window.navigator.getBattery"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function getBattery() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function getBattery() { [native code] }"
                        };
                        return res;
                    })();
                    prot["sendBeacon"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.sendBeacon"]) {
                                return aliases["window.navigator.sendBeacon"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function sendBeacon() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function sendBeacon() { [native code] }"
                        };
                        return res;
                    })();
                    prot["getGamepads"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.getGamepads"]) {
                                return aliases["window.navigator.getGamepads"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function getGamepads() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function getGamepads() { [native code] }"
                        };
                        return res;
                    })();
                    prot["javaEnabled"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.javaEnabled"]) {
                                return aliases["window.navigator.javaEnabled"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function javaEnabled() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function javaEnabled() { [native code] }"
                        };
                        return res;
                    })();
                    prot["vibrate"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.vibrate"]) {
                                return aliases["window.navigator.vibrate"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function vibrate() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function vibrate() { [native code] }"
                        };
                        return res;
                    })();
                    prot["userActivation"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object UserActivation]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object UserActivation]"
                                }
                            }
                        });
                        prot["hasBeenActive"] = false;
                        prot["isActive"] = false;
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["mediaSession"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object MediaSession]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object MediaSession]"
                                }
                            }
                        });
                        prot["metadata"] = null;
                        prot["playbackState"] = "none";
                        prot["setActionHandler"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function setActionHandler() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function setActionHandler() { [native code] }"
                            };
                            return res;
                        })();
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["permissions"] = (function () {
                        var res = aliases["window.navigator.permissions"];
                        return res;
                    })();;
                    prot["clipboard"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object Clipboard]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object Clipboard]"
                                }
                            }
                        });
                        prot["read"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["readText"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["write"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["writeText"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["addEventListener"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function addEventListener() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function addEventListener() { [native code] }"
                            };
                            return res;
                        })();
                        prot["removeEventListener"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function removeEventListener() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function removeEventListener() { [native code] }"
                            };
                            return res;
                        })();
                        prot["dispatchEvent"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function dispatchEvent() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function dispatchEvent() { [native code] }"
                            };
                            return res;
                        })();
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["credentials"] = (function () {
                        var res = aliases["window.navigator.credentials"];
                        return res;
                    })();;
                    prot["keyboard"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object Keyboard]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object Keyboard]"
                                }
                            }
                        });
                        prot["lock"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["unlock"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["getLayoutMap"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["locks"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object LockManager]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object LockManager]"
                                }
                            }
                        });
                        prot["request"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["query"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["mediaDevices"] = (function () {
                        var res = aliases["window.navigator.mediaDevices"];
                        return res;
                    })();;
                    prot["serviceWorker"] = (function () {
                        var res = aliases["window.navigator.serviceWorker"];
                        return res;
                    })();;
                    prot["storage"] = (function () {
                        var res = aliases["window.navigator.storage"];
                        return res;
                    })();;
                    prot["presentation"] = (function () {
                        var res = aliases["window.navigator.presentation"];
                        return res;
                    })();;
                    prot["bluetooth"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object Bluetooth]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object Bluetooth]"
                                }
                            }
                        });
                        prot["getAvailability"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["requestDevice"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["addEventListener"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function addEventListener() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function addEventListener() { [native code] }"
                            };
                            return res;
                        })();
                        prot["removeEventListener"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function removeEventListener() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function removeEventListener() { [native code] }"
                            };
                            return res;
                        })();
                        prot["dispatchEvent"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function dispatchEvent() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function dispatchEvent() { [native code] }"
                            };
                            return res;
                        })();
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["usb"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object USB]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object USB]"
                                }
                            }
                        });
                        prot["onconnect"] = null;
                        prot["ondisconnect"] = null;
                        prot["getDevices"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["requestDevice"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["addEventListener"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function addEventListener() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function addEventListener() { [native code] }"
                            };
                            return res;
                        })();
                        prot["removeEventListener"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function removeEventListener() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function removeEventListener() { [native code] }"
                            };
                            return res;
                        })();
                        prot["dispatchEvent"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function dispatchEvent() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function dispatchEvent() { [native code] }"
                            };
                            return res;
                        })();
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["xr"] = (function () {
                        var res = {};
                        var prot = {};
                        if (res) Object.defineProperty(res, 'toString', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object XR]"
                                }
                            }
                        });
                        if (res) Object.defineProperty(res, 'valueOf', {
                            configurable: true,
                            enumerable: false,
                            get: function () {
                                return function () {
                                    return "[object XR]"
                                }
                            }
                        });
                        prot["ondevicechange"] = null;
                        prot["supportsSession"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["isSessionSupported"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["requestSession"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function () { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function () { [native code] }"
                            };
                            return res;
                        })();
                        prot["addEventListener"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function addEventListener() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function addEventListener() { [native code] }"
                            };
                            return res;
                        })();
                        prot["removeEventListener"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function removeEventListener() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function removeEventListener() { [native code] }"
                            };
                            return res;
                        })();
                        prot["dispatchEvent"] = (function () {
                            var res = function () { };
                            res.toString = function () {
                                return "function dispatchEvent() { [native code] }"
                            };
                            res.valueOf = function () {
                                return "function dispatchEvent() { [native code] }"
                            };
                            return res;
                        })();
                        Object.setPrototypeOf(res, prot);
                        return res;
                    })();;
                    prot["setAppBadge"] = (function () {
                        var res = function () { };
                        res.toString = function () {
                            return "function () { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function () { [native code] }"
                        };
                        return res;
                    })();
                    prot["clearAppBadge"] = (function () {
                        var res = function () { };
                        res.toString = function () {
                            return "function () { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function () { [native code] }"
                        };
                        return res;
                    })();
                    prot["requestMediaKeySystemAccess"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.requestMediaKeySystemAccess"]) {
                                return aliases["window.navigator.requestMediaKeySystemAccess"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function () { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function () { [native code] }"
                        };
                        return res;
                    })();
                    prot["getUserMedia"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.getUserMedia"]) {
                                return aliases["window.navigator.getUserMedia"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function () { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function () { [native code] }"
                        };
                        return res;
                    })();
                    prot["webkitGetUserMedia"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.webkitGetUserMedia"]) {
                                return aliases["window.navigator.webkitGetUserMedia"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function () { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function () { [native code] }"
                        };
                        return res;
                    })();
                    prot["requestMIDIAccess"] = (function () {
                        var res = function () {
                            if (aliases["window.navigator.requestMIDIAccess"]) {
                                return aliases["window.navigator.requestMIDIAccess"].apply(aliases["window.navigator"], arguments)
                            }
                        };
                        res.toString = function () {
                            return "function () { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function () { [native code] }"
                        };
                        return res;
                    })();
                    prot["getInstalledRelatedApps"] = (function () {
                        var res = function () { };
                        res.toString = function () {
                            return "function () { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function () { [native code] }"
                        };
                        return res;
                    })();
                    Object.setPrototypeOf(res, prot);
                    return res;
                })();;
                return resp;
            }
        })
    })();;
    (function () {
        var resp = null;
        delete window.navigator.plugins;
        Object.defineProperty(window.navigator, "plugins", {
            configurable: true,
            enumerable: false,
            get: function () {
                if (resp) return resp;
                resp = (function () {
                    var res = {};
                    var prot = {};
                    if (res) Object.defineProperty(res, 'toString', {
                        configurable: true,
                        enumerable: false,
                        get: function () {
                            return function () {
                                return "[object PluginArray]"
                            }
                        }
                    });
                    if (res) Object.defineProperty(res, 'valueOf', {
                        configurable: true,
                        enumerable: false,
                        get: function () {
                            return function () {
                                return "[object PluginArray]"
                            }
                        }
                    });
                    prot["length"] = 0;
                    prot["refresh"] = (function () {
                        var res = function () { };
                        res.toString = function () {
                            return "function refresh() {\\n    [native code]\\n}"
                        };
                        res.valueOf = function () {
                            return "function refresh() {\\n    [native code]\\n}"
                        };
                        return res;
                    })();
                    prot["namedItem"] = (function () {
                        var res = function (item) {
                            var length = window.navigator.plugins.length;
                            for (var i = 0; i < length; i++) {
                                var p = window.navigator.plugins[i];
                                if (p.name == item) return p
                            }
                        };
                        res.toString = function () {
                            return "function namedItem() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function namedItem() { [native code] }"
                        };
                        return res;
                    })();
                    prot["item"] = (function () {
                        var res = function (item) {
                            return window.navigator.plugins[item]
                        };
                        res.toString = function () {
                            return "function item() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function item() { [native code] }"
                        };
                        return res;
                    })();
                    Object.setPrototypeOf(res, prot);
                    return res;
                })();;
                return resp;
            }
        })
    })();;
    (function () {
        var resp = null;
        delete window.navigator.mimeTypes;
        Object.defineProperty(window.navigator, "mimeTypes", {
            configurable: true,
            enumerable: false,
            get: function () {
                if (resp) return resp;
                resp = (function () {
                    var res = {};
                    var prot = {};
                    if (res) Object.defineProperty(res, 'toString', {
                        configurable: true,
                        enumerable: false,
                        get: function () {
                            return function () {
                                return "[object MimeTypeArray]"
                            }
                        }
                    });
                    if (res) Object.defineProperty(res, 'valueOf', {
                        configurable: true,
                        enumerable: false,
                        get: function () {
                            return function () {
                                return "[object MimeTypeArray]"
                            }
                        }
                    });
                    prot["length"] = 0;
                    prot["item"] = (function () {
                        var res = function (item) {
                            return window.navigator.mimeTypes[item]
                        };
                        res.toString = function () {
                            return "function item() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function item() { [native code] }"
                        };
                        return res;
                    })();
                    prot["namedItem"] = (function () {
                        var res = function (item) {
                            var length = window.navigator.mimeTypes.length;
                            for (var i = 0; i < length; i++) {
                                var p = window.navigator.mimeTypes[i];
                                if (p.type == item) return p
                            }
                        };
                        res.toString = function () {
                            return "function namedItem() { [native code] }"
                        };
                        res.valueOf = function () {
                            return "function namedItem() { [native code] }"
                        };
                        return res;
                    })();
                    Object.setPrototypeOf(res, prot);
                    return res;
                })();;
                return resp;
            }
        })
    })();;
    (function () {
        var resp = null;
        delete window.navigator.hardwareConcurrency;
        Object.defineProperty(window.navigator, "hardwareConcurrency", {
            configurable: true,
            enumerable: false,
            get: function () {
                if (resp) return resp;
                resp = 8;
                return resp;
            }
        })
    })();
})();
try {
    delete window["webkitSpeechRecognitionEvent"];
} catch (e) { }
try {
    delete window["webkitSpeechRecognitionError"];
} catch (e) { }
try {
    delete window["webkitSpeechRecognition"];
} catch (e) { }
try {
    delete window["webkitSpeechGrammarList"];
} catch (e) { }
try {
    delete window["webkitSpeechGrammar"];
} catch (e) { }
try {
    delete window["webkitOfflineAudioContext"];
} catch (e) { }
try {
    delete window["webkitAudioContext"];
} catch (e) { }
try {
    delete window["webkitStorageInfo"];
} catch (e) { }
try {
    delete window["webkitRTCPeerConnection"];
} catch (e) { }
try {
    delete window["webkitMediaStream"];
} catch (e) { }
try {
    delete window["webkitIDBTransaction"];
} catch (e) { }
try {
    delete window["webkitIDBRequest"];
} catch (e) { }
try {
    delete window["webkitIDBObjectStore"];
} catch (e) { }
try {
    delete window["webkitIDBKeyRange"];
} catch (e) { }
try {
    delete window["webkitIDBIndex"];
} catch (e) { }
try {
    delete window["webkitIDBFactory"];
} catch (e) { }
try {
    delete window["webkitIDBDatabase"];
} catch (e) { }
try {
    delete window["webkitIDBCursor"];
} catch (e) { }
try {
    delete window["webkitIndexedDB"];
} catch (e) { }
try {
    delete window["WebKitCSSMatrix"];
} catch (e) { }
try {
    delete window["WebKitMutationObserver"];
} catch (e) { }
try {
    delete window["webkitURL"];
} catch (e) { }
try {
    delete window["WebKitAnimationEvent"];
} catch (e) { }
try {
    delete window["WebKitTransitionEvent"];
} catch (e) { }
try {
    delete window["webkitRequestAnimationFrame"];
} catch (e) { }
try {
    delete window["webkitCancelAnimationFrame"];
} catch (e) { }
try {
    delete window["webkitCancelRequestAnimationFrame"];
} catch (e) { }
try {
    delete window["webkitRequestFileSystem"];
} catch (e) { }
try {
    delete window["webkitResolveLocalFileSystemURL"];
} catch (e) { }