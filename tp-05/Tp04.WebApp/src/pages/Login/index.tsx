import React, { useCallback, useEffect, useState } from "react";
import { FiSettings } from "react-icons/fi";
import { AiOutlineCrown } from "react-icons/ai";
import { BsBookmarkCheck } from "react-icons/bs";
import { MdCompareArrows } from "react-icons/md";

import Input from "../../components/Input";
import NavBar from "../../components/NavBar";
import Button from "../../components/Button";

import * as SC from "./styles";
import { BiBook, BiUser } from "react-icons/bi";
import axios from "axios";
import apiConfig from "../../config/api";
import { useHistory } from "react-router";
import { routesMap } from "../../config/routes-map";
import { useLocation } from "react-router-dom";
import IBook from "../../IBook";

const Login: React.FC = () => {
  const [bookName, setBookName] = useState("");
  const [bookPassword, setBookPassword] = useState("");
  const [bookStatus, setBookStatus] = useState(true);
  const [bookId, setBookId] = useState<number | null>(null);
  const history = useHistory();
  const queryParams = new URLSearchParams(useLocation().search);

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (!token) {
      history.push(routesMap.Login.path);
    } else {
      axios
        .get<IBook[]>(`${apiConfig.baseUrl}/sessions/check`, {
          headers: { Authorization: `Bearer ${token}` },
        })
        .then(() => history.push(routesMap.Home.path));
    }
  }, []);

  const handleSaveSettings = useCallback(async () => {
    const book = {
      nome: bookName,
      senha: bookPassword,
    };

    try {
      const {
        data: { token },
      } = await axios.post<{ token: string }>(
        `${apiConfig.baseUrl}/sessions`,
        book
      );

      localStorage.setItem("token", token);

      history.push(routesMap.Home.path);
    } catch (error) {
      alert("Ooops");
    }
  }, [bookName, bookPassword]);

  return (
    <SC.Container>
      <h1>Login</h1>

      <SC.Content>
        <SC.SettingsGroup>
          <strong className="header">Faz login aí</strong>

          <Input
            value={bookName}
            icon={BiBook}
            placeholder="Nome"
            onChange={(event) => setBookName(event.target.value)}
          />

          <Input
            value={bookPassword}
            icon={BiUser}
            placeholder="Senha"
            onChange={(event) => setBookPassword(event.target.value)}
          />
        </SC.SettingsGroup>

        <SC.Footer>
          <Button onClick={handleSaveSettings}>Logar</Button>
        </SC.Footer>
      </SC.Content>
    </SC.Container>
  );
};

export default Login;
