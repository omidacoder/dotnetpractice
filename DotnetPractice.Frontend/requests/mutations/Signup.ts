/* eslint-disable prettier/prettier */
import axios from "axios";

import { siteConfig } from "@/config/site";

export const Signup = async({
  username,
  password,
  name,
  email
}: {
  username: string;
  password: string;
  name: string;
  email: string;
}) => {
  return await axios({
    url: siteConfig.requestUrls.baseUrl + siteConfig.requestUrls.signup,
    data: {
      UserName: username,
      Password: password,
      name: name.trim().length == 0 ? undefined : name,
      email: email.trim().length == 0 ? undefined : email
    },
    method: "post",
  });
};
