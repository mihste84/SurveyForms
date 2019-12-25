export const devSettings = {
    access_token: 'dev',
    id_token: 'dev',
    scope: 'dev',
    session_state: 'dev',
    expires_at: 99999999,
    profile: '',
    refresh_token: 'dev',
    state: '',
    token_type: 'dev'
};

export const authTestSettings = {
    authority: 'https://localhost:5000',
    client_id: 'angularclient',
    redirect_uri: `http://localhost:4200/authCallback`,
    response_type: 'code',
    scope: 'openid profile email WebAPI',
    post_logout_redirect_uri: 'http://localhost:4200/',
    loadUserInfo: false,
    automaticSilentRenew: true,
    extraQueryParams: {
      resource: '9114fb20-3c94-4bf5-bf2b-7481f96a103a'
    },
    silent_redirect_uri: `http://localhost:4200/silent-refresh.html`,
    includeIdTokenInSilentRenew: true
};

export const stagingSettings = {
    authority: 'https://login.microsoftonline.com/de7e7a67-ae61-49d2-97a7-526c910ad675',
    client_id: '9114fb20-3c94-4bf5-bf2b-7481f96a103a',
    redirect_uri: `http://localhost:4200/signin-callback.html`,
    response_type: 'id_token token',
    scope: 'openid profile email WebAPI',
    post_logout_redirect_uri: 'http://localhost:4200/signout-callback.html',
    loadUserInfo: false,
    automaticSilentRenew: true,
    extraQueryParams: {
      resource: '9114fb20-3c94-4bf5-bf2b-7481f96a103a'
    },
    silent_redirect_uri: `http://localhost:4200/signin-callback.html`,
    includeIdTokenInSilentRenew: true
};
