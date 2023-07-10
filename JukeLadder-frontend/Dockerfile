FROM node:17-alpine as builder

RUN mkdir -p /usr/src/jukeladder-frontend
WORKDIR /usr/src/jukeladder-frontend
COPY . /usr/src/jukeladder-frontend/

RUN npm install
RUN npm run build --dotenv=.env

FROM node:17-alpine

COPY --from=builder /usr/src/jukeladder-frontend/.output .
COPY --from=builder /usr/src/jukeladder-frontend/.env .

EXPOSE 3000

CMD ["node", "server/index.mjs"]